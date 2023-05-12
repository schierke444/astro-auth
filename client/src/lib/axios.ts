import axios, { Axios, AxiosError } from 'axios'
import type { AuthDetails } from '../types/User';

let refresh = false;


const client = axios.create({
    baseURL: "http://localhost:5243/api/v1",
    headers: {
        'Content-Type': 'application/json'
    },
    withCredentials: true
})

export const authClient = axios.create({
    baseURL: "http://localhost:5243/api/v1",
    headers: {
        'Content-Type': 'application/json'
    },
    withCredentials: true
})

client.interceptors.response.use((config) => {
    return config;
}, async (err) => {
    const prevRequest = err.config;
    // console.log(prevRequest.sent)
    if (err.response?.status === 401 && !prevRequest.sent) {
        try {
            prevRequest.sent = true
            const res = await axios.get<AuthDetails>('http://localhost:5243/api/v1/Auth/refresh', { withCredentials: true });
            if (res.status === 200) {
                // localStorage.setItem('access-token', res.data.accessToken);
                    client.defaults.headers.common['Authorization'] = `Bearer ${res.data.accessToken}`
                    prevRequest.headers.Authorization = `Bearer ${res.data.accessToken}`
            }
            // prevRequest.sent = false
            console.log(client.defaults)
            return client(prevRequest);
        }
        catch(err)
        {
            window.location.href = '/'
            return Promise.reject(err);
        }
    }
    return err
})

export default client