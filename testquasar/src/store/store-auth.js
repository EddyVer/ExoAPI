import { api } from 'boot/axios';
//import { stat } from 'fs';
import jwt_decode from 'jwt-decode';
//import { Loading, LocalStorage } from 'quasar';

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
    const decriptToken = jwt_decode(token);
    //state.token = decriptToken;
    console.log(decriptToken);
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
        commit();
        console.log(response.data.name);
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
        //console.log(response.data);
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
