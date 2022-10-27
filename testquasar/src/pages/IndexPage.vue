<template>
  <q-btn @click="show">form Add Product</q-btn>
  <div>
    <formadd v-if="formNewProduct" :formNewProduct="formNewProduct"></formadd>
  </div>
  <div class="row">
    <q-input v-model="dataId" />
    <q-btn label="search" color="primary" @click="getDataById" />
  </div>
  <div v-if="modifData">
    <formodif :modifData="modifData"></formodif>
  </div>
  <div class="row">
    <q-input type="number" v-model="dataId" />
    <q-btn label="delete" color="primary" @click="deleteProduct" />
  </div>
  <q-page class="row items-center justify-evenly">
    {{ datas }}
  </q-page>
</template>

<script lang="ts">
import { defineComponent, ref } from 'vue';
import { ProductDto } from 'src/models/product';

export default defineComponent({
  name: 'IndexPage',
  components: {
    formadd: require('components/formAdd.vue').default,
    formodif: require('components/formModif.vue').default,
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
    };
  },
  methods: {
    async firstApiCall() {
      const response = await this.$api.get('/products/ShowStock');
      this.datas = response.data;
    },
    async getDataById() {
      const resp = await this.$api.get(`products/GetById/${this.dataId}`);
      this.modifData = resp.data as ProductDto;
    },
    async deleteProduct() {
      const resp = await this.$api.delete(
        `products/deleteProduct/${this.dataId}`
      );
      return resp;
    },
    show() {
      this.formNewProduct = !this.formNewProduct;
    },
  },
});
</script>
