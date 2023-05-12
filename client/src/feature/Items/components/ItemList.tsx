import type { ItemDetails } from "../../../types/Items";
import Item from "./Item";

type ItemListType = {
    data: ItemDetails[]
}

const ItemList = ({data}: ItemListType) => {
    return (
        <ul>
            {data.map(v => {
                return <Item key={v.id} data={v} />
            })}
        </ul>
    )
}

export default ItemList;