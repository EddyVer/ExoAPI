<template>
  <q-layout view="hHh lpr fFf">
    <q-header elevated>
      <q-toolbar>
        <q-toolbar-title class="absolute-center"> Namezine </q-toolbar-title>
        <q-tabs class="absolute-right">
          <q-btn to="/" flat label="Home" class="" />
          <q-btn v-if="user" label="user.name" />
          <q-btn name="enregistrement" flat to="/sign" label="Sign Up" />
          <q-btn name="connexion" flat to="/login" label="Login" />
        </q-tabs>
      </q-toolbar>
    </q-header>
    <!-- important -->
    <q-page-container>
      <router-view />
    </q-page-container>
  </q-layout>
</template>

<script lang="ts">
import { User } from 'src/models/User';
import { defineComponent, ref } from 'vue';

export default defineComponent({
  name: 'MainLayout',

  components: {},

  setup() {
    const leftDrawerOpen = ref(false);

    return {
      user: ref<User>(),
      leftDrawerOpen,
      toggleLeftDrawer() {
        leftDrawerOpen.value = !leftDrawerOpen.value;
      },
    };
  },
});
</script>
