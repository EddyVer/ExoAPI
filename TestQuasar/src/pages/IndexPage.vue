<template>
  <q-page class="row items-center justify-evenly">
    {{ datas }}
  </q-page>
</template>

<script lang="ts">
import { defineComponent, ref } from 'vue';

export default defineComponent({
  name: 'IndexPage',
  created() {
    void this.firstApiCall();
  },

  setup() {
    return {
      datas: ref<any[]>([]),
    };
  },
  methods: {
    async firstApiCall() {
      const response = await this.$api.get('/products/ShowStock');
      this.datas = response.data;
    },
  },
});
</script>
