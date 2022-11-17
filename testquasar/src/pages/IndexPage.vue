<template>
  <q-layout view="hHh lpr fFf">
    <q-header elevated>
      <s-toolbar>
        <q-toolbar-titlet class="absolute-centre">
          Title Toolbar
        </q-toolbar-titlet>
        <q-btn
          to="/connexion"
          flat
          icon-right="account_circle"
          label="Se connecter"
          class="float-right"
        />
        <q-btn label="Sign Up" class="float-right" flat />
      </s-toolbar>
    </q-header>
  </q-layout>
  <q-div class="row" v-if="!user">
    <q-input placeholder="Name" v-model="name" />
    <q-input placeholder="Password" type="password" v-model="password" />
    <q-input placeholder="repeat password" type="password" v-model="password" />
    <q-btn label="sign up" @click="getUser"></q-btn>
  </q-div>
  <q-div class="row vertical-middle" v-if="!user">
    <q-input placeholder="Name" v-model="name" />
    <q-input placeholder="Password" type="password" v-model="password" />
    <q-btn label="Login" @click="getUser"></q-btn>
  </q-div>
  <q-div v-if="user">
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
    <div class="row">
      <q-input type="number" v-model="dataId" />
      <q-btn label="delete" color="primary" @click="deleteProduct" />
    </div>
  </q-div>
  <q-page class="row items-center justify-evenly">
    {{ datas }}
  </q-page>
</template>

<script lang="ts">
import { defineComponent, ref } from 'vue';
import { ProductDto } from 'src/models/product';
import { User } from 'src/models/User';
import formModif from 'src/components/formModif.vue';
import formData from 'src/components/formAdd.vue';
export default defineComponent({
  name: 'IndexPage',
  components: {
    formData,
    formModif,
  },
  created() {
    void this.firstApiCall();
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
    };
  },
  methods: {
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
