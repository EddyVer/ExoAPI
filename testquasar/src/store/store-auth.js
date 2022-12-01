import { api } from 'boot/axios';
import jwt_decode from 'jwt-decode';
import { Loading, LocalStorage } from 'quasar';

const state = {
  user: null,
  role: null,
  token: null,
};

const mutations = {
  setUser(state, token) {
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

const actions = {
  registerUser({ commit }, form) {
    api
      .post('/Users/register', form)
      .then(function () {
        this.$router.push('/login');
      })
      .catch(function (error) {
        console.log(error.response);
      });
  },
  connectUser({ commit }, form) {
    // const token = (request) => {
    //   request.headers.Authorization = `bearer ${response.data}`;
    //   return request;
    // };
    api
      .post('/Users/login', form)
      .then((response) => {
        api.defaults.headers.common['Authorization'] =
          'bearer ' + response.data;
        commit('setUser', response.data);
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
    commit('setUser', null);
    LocalStorage.remove('user');
    LocalStorage.remove('token');
    LocalStorage.remove('role');
    //LocalStorage.clear();
    this.$router.push('/login');
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
