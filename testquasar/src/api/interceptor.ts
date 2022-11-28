// import { AxiosInstance, AxiosResponse } from 'axios'
//import useStore from 'src/store/auth-custom'
import { Router } from 'vue-router';
import { AxiosInstance, AxiosRequestConfig } from 'axios';

interface IAxiosError {
  response: IAxiosResponse;
}

interface IAxiosResponse {
  status: number;
  config: AxiosRequestConfig;
  //data: any;
}
export default (http: AxiosInstance, router: Router): void => {
  http.interceptors.request.use((config: AxiosRequestConfig) => {
    const token = localStorage.getItem('token') as string | undefined;
    // eslint-disable-next-line @typescript-eslint/no-unsafe-member-access
    if (token && !config.headers.common.Authorization) {
      // eslint-disable-next-line @typescript-eslint/no-unsafe-member-access
      config.headers.common.Authorization = `Bearer ${token}`;
      // eslint-disable-next-line @typescript-eslint/no-unsafe-member-access
      config.headers['Access-Control-Expose-Headers'] = 'Content-Disposition';
    }
    return config;
  });

  http.interceptors.response.use(
    (response) => response,
    async (error) => {
      const { response } = error as IAxiosError;
      if (!response) {
        return Promise.reject(error);
      }
      if ([401].includes(response.status)) {
        router.push('/login');
      }
      return Promise.reject(error);
    }
  );
};
