<template>
  <q-layout view="hHh lpr fFf">
    <q-header elevated>
      <q-toolbar>
        <q-toolbar-title class="absolute-center"> Namezine </q-toolbar-title>
        <q-tabs class="absolute-right">
          <q-btn to="/" flat label="Home" class="" />
          <q-div v-if="user">
            <q-p>{{ user }}</q-p>
            <q-btn @click="disconnectUser" flat label="Logout"></q-btn>
          </q-div>
          <q-div v-else>
            <q-btn name="enregistrement" flat to="/sign" label="Sign Up" />
            <q-btn name="connexion" flat to="/login" label="Login" />
          </q-div>
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
//import { LocalStorage } from 'quasar';
import { mapState, mapActions } from 'vuex';

export default {
  name: 'MainLayout',
  computed: {
    ...mapState('auth', ['user']),
  },

  setup() {
    return {
      leftDrawerOpen: false,
    };
  },
  methods: {
    ...mapActions('auth', ['disconnectUser']),
  },
};
</script>
