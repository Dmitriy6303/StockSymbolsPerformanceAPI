import axios from 'axios';

const axiosInstance = axios.create({
  baseURL: 'https://localhost:5025',
});

export { axiosInstance };
