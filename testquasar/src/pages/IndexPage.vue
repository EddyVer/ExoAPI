<template>
  <q-page class="items-center">
    <q-div v-if="user != null">
      <q-btn @click="DlFile" download label="Download File" />
      <q-btn @click="show">form Add Product</q-btn>
      <div>
        <form-data v-if="formNewProduct"></form-data>
      </div>
      <div class="row">
        <q-input v-model="dataId" />
        <q-btn label="search" color="primary" @click="getDataById" />
      </div>
      <div v-if="modifData">
        <formModif :modifData="modifData"></formModif>
      </div>
      <div v-if="role == 'Admin'" class="row">
        <q-input type="number" v-model="dataId" />
        <q-btn label="delete" color="primary" @click="deleteProduct" />
      </div>
    </q-div>
    <q-div class="row items-center justify-evenly">
      <q-p v-for="data in datas" :key="data.name">
        {{ data.name }}
        {{ data.quantite }}
      </q-p>
    </q-div>
    <q-div v-if="user" class="row fixed-center justify-evenly">
      <q-p v-for="item in weather" :key="item.tempertureC">
        {{ item.summary }}
      </q-p>
    </q-div>
  </q-page>
</template>

<script lang="ts">
import { defineComponent, ref } from 'vue';
import { ProductDto } from 'src/models/product';
import { User } from 'src/models/User';
import { Weathers } from 'src/models/weather';
//import { LocalStorage } from 'quasar';
import { mapState } from 'vuex';
//import { useQuasar } from 'quasar';
import formModif from 'src/components/formModif.vue';
import formData from 'src/components/formAdd.vue';
//import { response } from 'express';
import { exportFile } from 'quasar';
import { response } from 'express';
export default defineComponent({
  name: 'IndexPage',
  computed: {
    ...mapState('auth', ['user', 'role']),
  },
  components: {
    formData,
    formModif,
  },
  created() {
    void this.firstApiCall();
    void this.callAuthor();
    this.log();
  },
  setup() {
    return {
      datas: ref<ProductDto[]>([]),
      modifData: ref<ProductDto>(),
      dataId: 0,
      formNewProduct: ref<boolean>(false),
      formModif: ref<boolean>(false),
      userInfo: ref<User>(),
      name: '',
      password: '',
      weather: ref<Weathers[]>([]),
    };
  },
  methods: {
    async callAuthor() {
      const resp = await this.$api.get('/WeatherForecast');
      this.weather = resp.data;
    },
    async firstApiCall() {
      const response = await this.$api.get('/products/ShowStock');
      this.datas = response.data;
    },
    async getDataById() {
      this.modifData = undefined;
      const resp = await this.$api.get(`products/GetById/${this.dataId}`);

      this.modifData = resp.data as ProductDto;
    },
    async deleteProduct() {
      const resp = await this.$api.delete(
        `products/deleteProduct/${this.dataId}`
      );
      return resp;
    },
    async DlFile() {
      try {
        const resp = await this.$api.get('Users/files/test', {
          responseType: 'blob',
        });
        const status = exportFile('test', resp.data);
        if (!status) {
          console.log('Error: ' + status);
        }
      } catch (e) {
        console.log(e);
      }
    },
    // async getUser() {
    //   const resp = await this.$api.get(
    //     `GetByName/${this.name}/${this.password}`
    //   );
    //   this.user = resp.data;
    // },
    show() {
      this.formNewProduct = !this.formNewProduct;
    },
    log() {
      console.log(this.role);
    },
  },
});
</script>
