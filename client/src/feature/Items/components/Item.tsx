import type { ItemDetails } from "../../../types/Items"
import { useDeleteItem } from "../hooks/useDeleteItem"

type ItemType = {
    data: ItemDetails
}

const Item = ({data}: ItemType) => {
    const {mutate, isLoading} = useDeleteItem()
    const {id, name} = data
    return (
        <li className="mb-6 p-2 shadow-md">
            <div className="flex items-center justify-between">
                <p>{name}</p>
                <button
                onClick={() =>{mutate(id)}} 
                type="submit" 
                className="bg-red-500 text-white font-semibold rounded-md px-2 py-1">{isLoading ? 'Loading...' : 'Delete'}</button>
            </div>
        </li>
    )
}

export default Item