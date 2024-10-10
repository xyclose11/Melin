import { WorkspaceToolBar } from "@/routes/CustomComponents/WorkspaceToolBar.tsx";

export function Workspace() {
    return (
        <div className="flex">
            <WorkspaceToolBar />
            <div className="flex-1 p-4"></div>
        </div>
    );
}
