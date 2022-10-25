<template>
  <div class="row">
    <q-input placeholder="Origin" v-model="newData.origin" />
    <q-input placeholder="Name" v-model="newData.name" />
    <q-input placeholder="Quantity" v-model="newData.quantite" />
    <q-select
      placeholder="Usage"
      emit-value
      v-model="newData.usage"
      :options="options"
    />
    <q-btn label="Ajouter" color="primary" @click="addData" />
  </div>
  <div class="row">
    <q-input type="number" v-model="dataId" />
    <q-btn label="Recherche" color="primary" @click="getDataById" />
  </div>
  <div class="row" id="test">
    <q-input v-model="modifData.origin" />
    <q-input v-model="modifData.name" />
    <q-input v-model="modifData.quantite" />
    <q-input v-model="modifData.usage" />
    <q-btn label="Modif" color="primary" @click="modifProduct" />
  </div>
  <q-page class="row items-center justify-evenly">
    {{ datas }}
  </q-page>
</template>

<script lang="ts">
import { defineComponent, ref } from 'vue';
import { ProductDto, Usages } from 'src/models/product';

export default defineComponent({
  name: 'IndexPage',
  created() {
    void this.firstApiCall();
  },
  setup() {
    return {
      datas: ref<ProductDto[]>([]),
      newData: ref<ProductDto>({
        id: 0,
        name: '',
        origin: '',
        quantite: 0,
        usage: Usages.Autre,
      }),
      options: [
        {
          label: 'Autre',
          value: Usages.Autre,
        },
        {
          label: 'Transport',
          value: Usages.Transport,
        },
        {
          label: 'Informatique',
          value: Usages.Informatique,
        },
        {
          label: 'Recherche',
          value: Usages.Recherche,
        },
      ],
      modifData: ref<ProductDto>({
        id: 0,
        name: '',
        origin: '',
        quantite: 0,
        usage: Usages.Autre,
      }),
      dataId: 0,
      respDataId: ref<ProductDto[]>([]),
    };
  },
  methods: {
    async firstApiCall() {
      const response = await this.$api.get('/products/ShowStock');
      this.datas = response.data;
    },
    async addData() {
      const resp = await this.$api.post('/products/addProduct', this.newData);
      return resp;
    },
    async getDataById() {
      const resp = await this.$api.get(`products/GetById/${this.dataId}`);
      this.modifData = resp.data as ProductDto;
    },
    async modifProduct() {
      const resp = await this.$api.put(
        `/Products/EditProduct/${this.modifData.id}`,
        this.modifData
      );
      return resp;
    },
  },
});
</script>
