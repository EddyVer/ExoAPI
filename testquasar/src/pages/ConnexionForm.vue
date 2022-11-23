<template>
  <q-page padding>
    <q-card class="connexion">
      <q-tab-panel class="text-h6">Login</q-tab-panel>
      <q-form @submit.prevent="submitForm">
        <q-input outlined v-model="form.name" label="Name" class="q-my-md" />
        <q-input
          type="password"
          outlined
          v-model="form.password"
          label="Mot de passe"
          class="q-my-md"
          :rules="[(val) => val.length >= 4 || 'Minimum 4 caractère']"
          lazy-rules
        />
        <q-btn type="submit" color="primary" label="Login" />
      </q-form>
    </q-card>
  </q-page>
</template>

<script>
import { mapActions } from 'vuex';
export default {
  name: 'ConnexionForm',
  data() {
    return {
      form: {
        id: '0',
        name: '',
        password: '',
        grade: 'u',
      },
    };
  },
  methods: {
    ...mapActions('auth', ['connectUser']),
    submitForm() {
      this.connectUser(this.form);
    },
    
  },
  // validateEmail(email) {
  //   // Source : https://stackoverflow.com/questions/46155/how-to-validate-an-email-address-in-javascript
  //   const re =
  //     /^(([^<>()[\]\\.,;:\s@"]+(\.[^<>()[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
  //   return re.test(String(email).toLowerCase());
  // },
};
</script>

<style scoped>
.connexion {
  max-width: 500px;
  width: auto;
  margin: 0 auto;
}
</style>
