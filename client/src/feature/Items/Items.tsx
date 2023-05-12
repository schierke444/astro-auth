import { QueryProviderHoc } from "../../provider/QueryProviderHoc"
import ItemList from "./components/ItemList"
import { useGetItems } from "./hooks/useGetItems"

const Items = () => {
    const {data, isLoading} = useGetItems()

    if(!data || isLoading)
    {
        return <p className="text-center">Loading...</p>
    }
    return (
        <>
            <ItemList  data={data}/>
        </>
    )
}


export default QueryProviderHoc(Items) 