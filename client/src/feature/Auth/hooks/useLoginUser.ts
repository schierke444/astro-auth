import { useMutation } from '@tanstack/react-query'
import type { LoginInput } from '../../../types/User'
import { loginUser } from '../services/authAPI'
import client from '../../../lib/axios'
import { toast } from 'react-hot-toast'

export const useLoginUser = () => {
    return useMutation((data: LoginInput) => loginUser(data), {

        onMutate: (data) => {
            console.log(data)
        },
        onSuccess: (data) => {
            client.defaults.headers.common['Authorization'] = `Bearer ${data.accessToken}`
            toast.success('Login success 🎉')
            setTimeout(() => {
                window.location.href = '/dashboard'

            }, 500)
        },
        onError: (err) => {
            console.log(err)
            toast.error('Invalid Username or password please check again. 🎉')
        },
        retry: 1,
        retryDelay: 500
    })

}