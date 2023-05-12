export interface ItemDetails {
    id: string,
    name: string,
    userId: string,
    createdAt: string,
    updatedAt: string 
}

export interface AddItemInput {
    name: string
}