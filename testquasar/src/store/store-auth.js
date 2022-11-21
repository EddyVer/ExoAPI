import { api } from 'boot/axios';

// State : données du magasin
const state = {
  user: null,
  token: null,
};

/*
Mutations : méthode qui manipulent les données
Les mutations ne peuvent pas être asynchrones !!!
 */
const mutations = {
  setUser(state, user) {
    state.user = user;
  },
  setToken(state, token) {
    state.token = token;
  },
};

/*
Actions : méthodes du magasin qui font appel aux mutations
Elles peuvent être asynchrones !
 */
const actions = {
  registerUser({ commit }, payload) {
    api
      .post('/Users/register', payload)
      .then(function (response) {
        commit('setUser', response.data.user);
        commit('setToken', response.data.access_token);
        console.log(response);
      })
      .catch(function (error) {
        console.log(error.response);
      });
  },
  connectUser({ commit }, payload) {
    api
      .post('/Users/login', payload)
      .then(function (response) {
        commit('setUser', response.data.user);
        commit('setToken', response.data.access_token);
      })
      .catch(function (error) {
        console.log(error.response);
      });
  },
};

/*
Getters : retourne les données du magasin
Fonctionne comme les propriétés calculées
Sert à calculer, trier, filtrer ou formater les donneés
 */
const getters = {};

/*
Exporte les constantes, variables du fichier
On pourra ainsi les récupérer, les importer dans un autre fichier JS.

namespace: true, ajoute un namespace à notre objet retourné.
 */
export default {
  namespaced: true,
  state,
  mutations,
  actions,
  getters,
};
