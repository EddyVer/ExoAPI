<template>
  <div class="row">
    <q-input v-model="modif.origin" />
    <q-input v-model="modif.name" />
    <q-input v-model="modif.quantite" />
    <q-select
      placeholder="Usage"
      emit-value
      v-model="modif.usage"
      :options="options"
    />
    <q-btn label="Modif" color="primary" @click="modifProduct" />
  </div>
</template>
<script lang="ts">
import { defineComponent, ref } from 'vue';
import { ProductDto, Usages } from 'src/models/product';
export default defineComponent({
  name: 'formModif',
  props: ['modifData'],
  created() {
    this.modif.id = this.modifData.id;
    this.modif.origin = this.modifData.origin;
    this.modif.name = this.modifData.name;
    this.modif.quantite = this.modifData.quantite;
    this.modif.usage = this.modifData.usage;
  },
  setup() {
    return {
      modif: ref<ProductDto>({
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
    async modifProduct() {
      const resp = await this.$api.put(
        `/Products/EditProduct/${this.modif.id}`,
        this.modif
      );
      this.modif.id = 0;
      this.modif.name = '';
      this.modif.origin = '';
      this.modif.quantite = 0;
      this.modif.usage = 0;
      return resp;
    },
  },
});
</script>
