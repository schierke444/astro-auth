import { useMutation, useQueryClient } from "@tanstack/react-query"
import type { AddItemInput } from "../../../types/Items"
import { addItem } from "../services/itemsAPI"
import { toast } from "react-hot-toast"

export const useAddItem = () => {
    const queryClient = useQueryClient()
    return useMutation((data: AddItemInput) => addItem(data),
    {
        onSuccess: () => {
            toast.success('New Item Added 🎉')
            queryClient.invalidateQueries(['items'])
        },
        onError: () => {

        },
        retry: 1,
        retryDelay: 500,
    })
}