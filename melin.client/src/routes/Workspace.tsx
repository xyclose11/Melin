import { WorkspaceToolBar } from "@/routes/CustomComponents/WorkspaceToolBar.tsx";
import {
    ResizableHandle,
    ResizablePanel,
    ResizablePanelGroup,
} from "@/components/ui/resizable";
import { Outlet } from "react-router-dom";

export function Workspace() {
    return (
        <ResizablePanelGroup direction="horizontal">
            <ResizablePanel
                collapsible={true}
                defaultSize={5}
                maxSize={7}
                collapsedSize={3}
            >
                <WorkspaceToolBar />
            </ResizablePanel>
            <ResizableHandle withHandle className="w-1 m-1" />
            <ResizablePanel id="WorkspaceContent">
                <div className="flex p-4 justify-center flex-wrap">
                    <Outlet />
                </div>
            </ResizablePanel>
        </ResizablePanelGroup>
    );
}
