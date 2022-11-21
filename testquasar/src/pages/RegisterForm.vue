<template>
  <q-page padding>
    <q-card class="connexion">
      <q-tab-panel class="text-h6">Sign Up</q-tab-panel>
      <q-form @submit.prevent="submitForm">
        <q-input
          outlined
          v-model="form.name"
          label="User Name"
          class="q-my-md"
          :rules="[(val) => val.length >= 4 || 'Minimum 4 caractère']"
        />

        <q-input
          type="password"
          outlined
          v-model="form.password"
          label="password"
          class="q-my-md"
          :rules="[(val) => val.length >= 4 || 'Minimum 4 caractère']"
          lazy-rules
        />

        <q-input
          type="password"
          outlined
          v-model="confPassword"
          label="Confirm password"
          class="q-my-md"
          :rules="[(val) => val === form.password || 'not same password']"
          lazy-rules
        />

        <q-btn type="submit" color="primary" label="Sign Up" />
      </q-form>
    </q-card>
  </q-page>
</template>

<script>
import { mapActions } from 'vuex';

export default {
  name: 'EnregistrementForm',
  data() {
    return {
      confPassword: '',
      form: {
        id: '0',
        name: '',
        password: '',
        grade: 'u',
      },
    };
  },
  methods: {
    ...mapActions('auth', ['registerUser']),
    submitForm() {
      this.registerUser(this.form);
    },
  },
};
</script>

<style scoped>
.connexion {
  max-width: 500px;
  width: auto;
  margin: 0 auto;
}
</style>
