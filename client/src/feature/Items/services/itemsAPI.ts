import client from "../../../lib/axios"
import type { AddItemInput, ItemDetails } from "../../../types/Items"

export const getItems = async () => {
    const res = await client.get<ItemDetails[]>('/Items')

    return res.data
}

export const addItem = async (addItemInput: AddItemInput) => {
    const res = await client.post('/Items', addItemInput)

    return res
}


export const deleteItem = async (id: string) => {
    const res = await client.delete(`/Items/${id}`)

    return res
}