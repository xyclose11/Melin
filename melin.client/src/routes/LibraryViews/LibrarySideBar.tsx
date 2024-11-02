import {
    Card,
    CardContent,
    CardFooter,
    CardHeader,
    CardTitle,
} from "@/components/ui/card.tsx";
import { useDraggable } from "@dnd-kit/core";
import {
    ContextMenu,
    ContextMenuContent,
    ContextMenuSub,
    ContextMenuSubContent,
    ContextMenuSubTrigger,
    ContextMenuTrigger,
} from "@/components/ui/context-menu.tsx";

import { CreateGroupForm } from "@/routes/GroupComponents/CreateGroupForm.tsx";
import {Button} from "@/components/ui/button.tsx";

export function LibrarySideBar(props: any) {
    const { attributes, listeners, setNodeRef, transform } = useDraggable({
        id: "draggable",
    });
    const style = transform
        ? {
              transform: `translate3d(${transform.x}px, ${transform.y}px, 0)`,
          }
        : undefined;

    return (
        <>
            <Card ref={setNodeRef} style={style} {...listeners} {...attributes}>
                <CardHeader className={"flex-auto"}>
                    <CardTitle>Groups</CardTitle>
                <ContextMenu>
                    <ContextMenuTrigger>
                        <Button size={"sm"}>...</Button>
                    </ContextMenuTrigger>
                    <ContextMenuContent>
                        <ContextMenuSub>
                            <ContextMenuSubTrigger>
                                Create Group
                            </ContextMenuSubTrigger>
                            <ContextMenuSubContent>
                                <CreateGroupForm />
                            </ContextMenuSubContent>
                        </ContextMenuSub>
                    </ContextMenuContent>
                </ContextMenu>
                </CardHeader>
                    
                <CardContent>{props.children}</CardContent>
                
                <CardFooter></CardFooter>
            </Card>
        </>
    );
}
