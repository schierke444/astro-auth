import { FormEvent, useState } from "react"
import { useAddItem } from "../hooks/useAddItem"
import { QueryProviderHoc } from "../../../provider/QueryProviderHoc"

const AddItem = () => {
    const { mutate } = useAddItem()
    const [itemName, setItemName] = useState("")

    const submitHandler = (e: FormEvent<HTMLFormElement>) => {
        e.preventDefault();
        mutate({
            name: itemName
        })
    }

    return (
        <>
            <form className="flex flex-col gap-4" onSubmit={submitHandler}>
                <div>
                    <input placeholder="Enter Item Name..." className="border border-gray-300 outline-none p-2 w-full" onChange={(e) => { setItemName(e.target.value) }} />
                </div>
                <div>
                    <button type="submit" className="bg-red-500 text-white font-semibold rounded-md px-2 py-1 w-full">Add Item</button>
                </div>
            </form>
        </>
    )
}

export default QueryProviderHoc(AddItem) 