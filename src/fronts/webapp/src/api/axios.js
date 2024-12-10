import axios from 'axios';

const catalogApiClient = axios.create({
  baseURL: 'https://localhost:3030',
  headers: {
    'Content-Type': 'application/json',
  },
});

export { catalogApiClient };
