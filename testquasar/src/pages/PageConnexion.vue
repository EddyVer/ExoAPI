<template>
  <!-- Not Use -->
  <q-page padding>
    <q-card class="connexion">
      <q-separator />

      <q-tab-panels v-model="tab" animated>
        <q-tab-panel name="connexion">
          <q-tab-panel class="text-h6">Se connecter</q-tab-panel>
          <connexion-form />
        </q-tab-panel>

        <q-tab-panel name="enregistrement">
          <q-tab-panel class="text-h6">Créer un compte</q-tab-panel>
          <register-form />
        </q-tab-panel>
      </q-tab-panels>
    </q-card>
  </q-page>
</template>

<script>
//import formData from 'src/components/formAdd.vue';
import ConnexionForm from 'src/components/Connexion/ConnexionForm.vue';
import RegisterForm from 'src/components/Connexion/RegisterForm.vue';
export default {
  name: 'PageConnexion',
  data() {
    return {
      tab: 'connexion',
    };
  },
  components: {
    ConnexionForm,
    RegisterForm,
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
