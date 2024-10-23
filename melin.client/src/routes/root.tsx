import { NavBar } from "@/routes/Layout.tsx";
import { Workspace } from "@/routes/Workspace.tsx";

export default function Root() {
    return (
        <>
            <NavBar />
            <div
                className={
                    "flex mt-16 justify-center items-center w-screen overflow-auto"
                }
            >
                <Workspace />
            </div>
        </>
    );
}
