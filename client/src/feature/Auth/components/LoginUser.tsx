import { FormEvent, useState } from "react"
import { useLoginUser } from "../hooks/useLoginUser"
import { QueryProviderHoc } from "../../../provider/QueryProviderHoc"

const LoginUser = () => {
    const {mutate, isLoading} = useLoginUser()
    const [username, setUsername] = useState("")
    const [password, setPassword] = useState("")


    return (
        <>
            <form className="flex flex-col gap-4" onSubmit={async (e) => {
                e.preventDefault();
                mutate({
                    username,
                    password
                })
            }}>
                <div>
                    <input placeholder="Enter Username" className="border border-gray-300 p-2 w-full" onChange={(e) => { setUsername(e.target.value) }} />
                </div>
                <div>
                    <input placeholder="Enter Password" className="border border-gray-300 p-2 w-full" onChange={(e) => { setPassword(e.target.value) }} />
                </div>
                <div>
                    <button type="submit" className="bg-red-500 text-white w-full p-2 rounded-md">Login</button>
                </div>
            </form>
        </>
    )
}


export default QueryProviderHoc(LoginUser) 