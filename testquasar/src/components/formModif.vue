<template>
  <div class="row">
    <q-input v-model="modif.origin" />
    <q-input v-model="modif.name" />
    <q-input type="number" v-model="modif.quantite" />
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
  props: {
    modifData: {
      type: Object as () => ProductDto,
      required: true,
    },
  },
  created() {
    this.modif = this.modifData;
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
      if (!this.modif) {
        return;
      }
      const resp = await this.$api.put(
        `/Products/EditProduct/${this.modif.id}`,
        this.modif
      );
      return resp;
    },
  },
});
</script>
