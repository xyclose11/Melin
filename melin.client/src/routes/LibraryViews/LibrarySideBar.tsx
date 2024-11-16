import {
    Card,
    CardContent,
    CardFooter,
    CardHeader,
    CardTitle,
} from "@/components/ui/card.tsx";
import { useDraggable } from "@dnd-kit/core";

import { CreateGroupForm } from "@/routes/GroupComponents/CreateGroupForm.tsx";
import {
    DropdownMenu,
    DropdownMenuContent,
    DropdownMenuSub,
    DropdownMenuSubContent,
    DropdownMenuSubTrigger,
    DropdownMenuTrigger,
} from "@/components/ui/dropdown-menu.tsx";
import { EllipsisVertical } from "lucide-react";

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
                <CardHeader className={"flex"}>
                    <CardTitle>Groups</CardTitle>
                    <DropdownMenu>
                        <DropdownMenuTrigger>
                            <EllipsisVertical size={14} />
                        </DropdownMenuTrigger>
                        <DropdownMenuContent>
                            <DropdownMenuSub>
                                <DropdownMenuSubTrigger>
                                    Create Group
                                </DropdownMenuSubTrigger>
                                <DropdownMenuSubContent>
                                    <CreateGroupForm />
                                </DropdownMenuSubContent>
                            </DropdownMenuSub>
                        </DropdownMenuContent>
                    </DropdownMenu>
                </CardHeader>

                <CardContent>{props.children}</CardContent>

                <CardFooter></CardFooter>
            </Card>
        </>
    );
}
