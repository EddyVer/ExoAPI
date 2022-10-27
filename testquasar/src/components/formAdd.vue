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
    <q-btn label="add" color="primary" @click="addData" />
  </div>
</template>
<script lang="ts">
import { defineComponent, ref } from 'vue';
import { ProductDto, Usages } from 'src/models/product';

export default defineComponent({
  name: 'formAdd',
  props: ['formNewProduct'],
  data() {
    return {};
  },
  setup() {
    return {
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
    };
  },
  methods: {
    async addData() {
      const resp = await this.$api.post('/products/addProduct', this.newData);
      return resp;
    },
  },
});
</script>
