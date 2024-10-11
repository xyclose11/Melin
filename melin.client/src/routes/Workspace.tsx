import { WorkspaceToolBar } from "@/routes/CustomComponents/WorkspaceToolBar.tsx";
import {
    ResizableHandle,
    ResizablePanel,
    ResizablePanelGroup,
} from "@/components/ui/resizable";
import { ReferenceCreationPage } from "@/routes/ReferenceCreationPage.tsx";

export function Workspace() {
    return (
        <ResizablePanelGroup direction="horizontal">
            <ResizablePanel
                collapsible={true}
                defaultSize={3}
                maxSize={6}
                collapsedSize={2}
            >
                <WorkspaceToolBar />
            </ResizablePanel>
            <ResizableHandle withHandle className="w-1 m-1" />
            <ResizablePanel>
                <div className="flex p-4 justify-center flex-wrap">
                    <ReferenceCreationPage />
                </div>
            </ResizablePanel>
        </ResizablePanelGroup>
    );
}
