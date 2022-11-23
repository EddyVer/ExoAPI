<template>
  <!-- <q-div> -->
  <!-- <q-btn @click="show">form Add Product</q-btn>
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
    <div class="row">
      <q-input type="number" v-model="dataId" />
      <q-btn label="delete" color="primary" @click="deleteProduct" />
    </div>
  </q-div> -->
  <q-page class="items-center">
    <q-div class="row items-center justify-evenly">
      <q-p v-for="data in datas" :key="data.name">
        {{ data.name }}
        {{ data.quantite }}
      </q-p>
    </q-div>
    <q-div class="row fixed-center items-center justify-evenly">
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
// import formModif from 'src/components/formModif.vue';
// import formData from 'src/components/formAdd.vue';
export default defineComponent({
  name: 'IndexPage',
  components: {
    // formData,
    // formModif,
  },
  created() {
    void this.firstApiCall();
    void this.callAuthor();
  },
  setup() {
    return {
      datas: ref<ProductDto[]>([]),
      modifData: ref<ProductDto>(),
      dataId: 0,
      formNewProduct: ref<boolean>(false),
      formModif: ref<boolean>(false),
      user: ref<User>(),
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
    async getUser() {
      const resp = await this.$api.get(
        `GetByName/${this.name}/${this.password}`
      );
      this.user = resp.data;
    },
    show() {
      this.formNewProduct = !this.formNewProduct;
    },
  },
});
</script>
