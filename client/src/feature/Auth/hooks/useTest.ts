import { useQuery } from "@tanstack/react-query"
import client from "../../../lib/axios"

export const useTest = () => {
    return useQuery(['test'], async() => {
        const res = await client.get<{message: string}>('/Test')
        return res.data
    },{
        retry: 1,
        retryDelay: 500,
        refetchOnWindowFocus: false
    })
}