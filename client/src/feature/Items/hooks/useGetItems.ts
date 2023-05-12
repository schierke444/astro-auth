import { useQuery } from "@tanstack/react-query"
import { getItems } from "../services/itemsAPI"

export const useGetItems = () => {
    return useQuery(['items'], () => getItems(), 
    {
        onSuccess: (data) => {
        },
        retry: 1,
        retryDelay: 500,
        refetchOnWindowFocus: false
    })
}