import { NavBar } from "@/routes/Layout.tsx";
import { Workspace } from "@/routes/Workspace.tsx";
import { WorkspaceToolBar } from "@/routes/CustomComponents/WorkspaceToolBar.tsx";
import {
    ResizableHandle,
    ResizablePanel,
    ResizablePanelGroup,
} from "@/components/ui/resizable";

export default function Root() {
    return (
        <div className={"mt-16 min-h-dvh"}>
            <NavBar />
            <ResizablePanelGroup  direction="horizontal">
                <ResizablePanel
                    collapsible={true}
                    defaultSize={5}
                    maxSize={7}
                    collapsedSize={3}
                >
                    <WorkspaceToolBar />
                </ResizablePanel>
                <ResizableHandle withHandle className="w-1 m-1" />
                <div
                    className={
                        "flex justify-center items-center w-screen"
                    }
                >
                    <ResizablePanel id="WorkspaceContent">
                        <Workspace />
                    </ResizablePanel>
                </div>
            </ResizablePanelGroup>
        </div>
    );
}
