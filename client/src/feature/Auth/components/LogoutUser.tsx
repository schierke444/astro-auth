import { useEffect } from "react"
import { QueryProviderHoc } from "../../../provider/QueryProviderHoc"
import { useLogoutUser } from "../hooks/useLogoutUser"

const LogoutUser = () => {
    const {mutate, isLoading, isSuccess} = useLogoutUser()
    return (
        <button onClick={() => {mutate()}} type="submit" className="bg-red-500 text-white font-semibold rounded-md px-2 py-1 w-full">
            {isLoading ? 'Loading...' : 'Logout'}
        </button>
    )
}

export default QueryProviderHoc(LogoutUser)