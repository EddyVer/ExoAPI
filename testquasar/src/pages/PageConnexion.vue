<template>
  <q-page padding>
    <q-card class="connexion">
      <q-tabs
        v-model="tab"
        class="text-grey"
        active-color="primary"
        indicator-color="primary"
        narrow-indicator
      >
        <q-tab name="connexion" label="Se connecter" />
        <q-tab name="enregistrement" label="Créer un compte" />
      </q-tabs>

      <q-separator />

      <q-tab-panels v-model="tab" animated>
        <q-tab-panel name="connexion">
          <div class="text-h6">Se connecter</div>
          <q-div class="row vertical-middle" v-if="!user">
            <q-input placeholder="Name" v-model="name" />
            <q-input
              placeholder="Password"
              type="password"
              v-model="password"
            />
            <q-btn label="Login" @click="getUser"></q-btn>
          </q-div>
        </q-tab-panel>

        <q-tab-panel name="enregistrement">
          <div class="text-h6">Créer un compte</div>
          <q-div class="row" v-if="!user">
            <q-input placeholder="Name" v-model="name" />
            <q-input
              placeholder="Password"
              type="password"
              v-model="password"
            />
            <q-input
              placeholder="repeat password"
              type="password"
              v-model="password"
            />
            <q-btn label="sign up" @click="getUser"></q-btn>
          </q-div>
        </q-tab-panel>
      </q-tab-panels>
    </q-card>
  </q-page>
</template>

<script>
export default {
  name: 'PageConnexion',
  data() {
    return {
      tab: 'connexion',
    };
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
