import { WorkspaceToolBar } from "@/routes/CustomComponents/WorkspaceToolBar.tsx";
import { CreateReference } from "@/routes/CustomComponents/CreateReference.tsx";
import {
    ResizableHandle,
    ResizablePanel,
    ResizablePanelGroup,
} from "@/components/ui/resizable";

export function Workspace() {
    return (
        <ResizablePanelGroup direction="horizontal">
            <ResizablePanel collapsible={true} collapsedSize={14}>
                <WorkspaceToolBar />
            </ResizablePanel>
            <ResizableHandle withHandle />
            <ResizablePanel>
                <div className="flex top-32">
                    <div className="flex-1 p-4">
                        <CreateReference />
                    </div>
                </div>
            </ResizablePanel>
        </ResizablePanelGroup>
    );
}
