import { useMutation } from "@tanstack/react-query"
import { logoutUser } from "../services/authAPI"
import client from "../../../lib/axios"
import { toast } from "react-hot-toast"

export const useLogoutUser = () => {
    return useMutation(() => logoutUser(),
    {
        onSuccess: () => {
            client.defaults.headers.common['Authorization'] = ""
            toast.success('User logged out 👋')

               setTimeout(() => {
                window.location.href = '/'

            }, 500)
        },
        onError: () => {

        },
        retry: 1,
        retryDelay: 500
    })
}