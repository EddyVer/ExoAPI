import { api } from 'boot/axios';
//import { state } from 'fs';
//import { stat } from 'fs';
import jwt_decode from 'jwt-decode';
import { Loading, LocalStorage } from 'quasar';

// State : données du magasin
const state = {
  user: null,
  role: null,
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
    if (token != null) {
      const decriptToken = jwt_decode(token);
      state.user = decriptToken.iss;
      state.role = decriptToken.aud;
    } else {
      state.user = token;
      state.role = token;
    }
    state.token = token;
  },
};

/*
Actions : méthodes du magasin qui font appel aux mutations
Elles peuvent être asynchrones !
 */
const actions = {
  registerUser({ commit }, form) {
    api
      .post('/Users/register', form)
      .then(function (response) {
        commit('setUser', response.data.name);
      })
      .catch(function (error) {
        console.log(error.response);
      });
  },
  connectUser({ commit }, form) {
    api
      .post('/Users/login', form)
      .then((response) => {
        api.defaults.headers.common['Authorization'] =
          'bearer ' + response.data;
        commit('setToken', response.data);
        LocalStorage.set('user', state.user);
        LocalStorage.set('token', state.token);
        LocalStorage.set('role', state.role);
        this.$router.push('/');
        Loading.hide();
      })
      .catch(function (error) {
        console.log(error.response);
      });
  },
  disconnectUser({ commit }) {
    Loading.show();
    // const authorazi = {
    //   headers: { Authorization: 'bearer ' + state.token },
    // };
    commit('setToken', null);
    LocalStorage.remove('user');
    LocalStorage.remove('token');
    LocalStorage.remove('role');
    //LocalStorage.clear();
    this.$router.push('/');
    Loading.hide();
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
