import { useMutation, useQueryClient } from "@tanstack/react-query"
import { deleteItem } from "../services/itemsAPI"
import { toast } from "react-hot-toast"

export const useDeleteItem = () => {
    const queryClient = useQueryClient()
    return useMutation((id: string) => deleteItem(id), {
        onSuccess: () => {
            toast.success('Item Deleted 👋')
            queryClient.invalidateQueries(['items'])
        },
        onError: () =>{

        },
        retry: 1,
        retryDelay: 500
    })

}