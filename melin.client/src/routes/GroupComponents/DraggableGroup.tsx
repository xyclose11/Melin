import {
    Card,
    CardContent,
    CardHeader,
    CardTitle,
} from "@/components/ui/card.tsx";
import { z } from "zod";
import { ChevronRight, EllipsisVertical } from "lucide-react";
import { EditGroupForm } from "@/routes/GroupComponents/EditGroupForm.tsx";
import { Button } from "@/components/ui/button.tsx";
import {
    Dialog,
    DialogContent,
    DialogDescription,
    DialogFooter,
    DialogHeader,
    DialogTitle,
    DialogTrigger,
} from "@/components/ui/dialog.tsx";
import { useReferenceSelection } from "@/routes/Context/ReferencesSelectedContext.tsx";
import { instance } from "@/utils/axiosInstance.ts";
import {
    DropdownMenu,
    DropdownMenuContent,
    DropdownMenuItem,
    DropdownMenuTrigger,
    DropdownMenuSeparator,
    DropdownMenuSubContent,
    DropdownMenuSubTrigger,
    DropdownMenuSub,
} from "@/components/ui/dropdown-menu.tsx";
import { useToast } from "@/hooks/use-toast.ts";
import { ToastAction } from "@/components/ui/toast.tsx";
import {
    Collapsible,
    CollapsibleContent,
    CollapsibleTrigger,
} from "@/components/ui/collapsible.tsx";
import { SidebarMenuButton } from "@/components/ui/sidebar.tsx";
import { useDraggable, useDroppable } from "@dnd-kit/core";
import {
    AlertDialog,
    AlertDialogAction,
    AlertDialogCancel,
    AlertDialogContent,
    AlertDialogDescription,
    AlertDialogFooter,
    AlertDialogHeader,
    AlertDialogTitle,
    AlertDialogTrigger,
} from "@/components/ui/alert-dialog.tsx";
import { GroupType } from "@/routes/LibraryPage.tsx";
import { useGroupSelection } from "@/routes/Context/SelectedGroupContext.tsx";
import { Checkbox } from "@/components/ui/checkbox.tsx";

const GroupReferenceSchema = z.object({
    id: z.number(),
    title: z.string(),
});

type GroupReferenceSchema = z.infer<typeof GroupReferenceSchema>;

export function DraggableGroup({
    groupName,
    references,
    groups,
}: {
    groupName: string;
    groups: [];
    references: [];
}) {
    const { selectedReferences } = useReferenceSelection();
    const { toggleGroup } = useGroupSelection();

    const { toast } = useToast();

    const { isOver, setNodeRef: setDropNodeRef } = useDroppable({
        id: groupName.concat(".drop"),
    });

    const {
        attributes,
        listeners,
        setNodeRef: setDragNodeRef,
        setActivatorNodeRef,
        transform,
    } = useDraggable({
        id: groupName.concat(".drag"),
    });

    const dragStyle = transform
        ? {
              transform: `translate3d(${transform.x}px, ${transform.y}px, 0)`,
          }
        : undefined;

    const dropStyle = {
        color: isOver ? "green" : undefined,
    };

    const handleAddReferences = async () => {
        try {
            const res = await instance.post(
                `add-refs-to-group?groupName=${groupName}`,
                selectedReferences,
                {
                    withCredentials: true,
                },
            );

            if (res.status === 200) {
                toast("", {
                    variant: "default",
                    title: "Reference Added to Group",
                    description: `Reference(s) Successfully Added to Group: ${groupName}`,
                });
            } else {
                toast("", {
                    variant: "destructive",
                    title: "Reference Unable to be added to Group",
                    description: `This reference is already in the group`,
                    action: (
                        <ToastAction altText={"Try Again"}>
                            Try Again
                        </ToastAction>
                    ),
                });
            }
        } catch (e) {
            toast("", {
                variant: "destructive",
                title: "Reference Unable to be added to Group",
                description: `Reference(s) Failed to be Added to Group: ${groupName}`,
                action: (
                    <ToastAction altText={"Try Again"}>Try Again</ToastAction>
                ),
            });
            console.error(e);
        }
    };

    const handleDeleteGroup = async () => {
        try {
            const res = await instance.delete(
                `delete-group?groupName=${groupName}`,
                {
                    withCredentials: true,
                },
            );

            if (res.status === 200) {
                toast("", {
                    variant: "default",
                    title: `Group: ${groupName} successfully deleted`,
                    description: "NOTE: Undo feature is currently in progress",
                    action: <ToastAction altText={"Undo"}>Undo</ToastAction>,
                });
            } else {
                toast("", {
                    variant: "destructive",
                    title: `Unable to Delete Group: ${groupName}`,
                    description:
                        "NOTE: Try Again feature is currently in progress",
                    action: (
                        <ToastAction altText={"Try Again"}>
                            Try Again
                        </ToastAction>
                    ),
                });
            }
        } catch (e) {
            toast("", {
                variant: "destructive",
                title: `CRITICAL ERROR ATTEMPTING TO DELETE GROUP: ${groupName}`,
                description: `ERROR: ${e}`,
            });
            console.error(e);
        }
    };

    return (
        <>
            <Card
                ref={setDragNodeRef}
                style={dragStyle}
                {...attributes}
                className={"mb-2"}
            >
                <CardHeader className={"flex"}>
                    <CardTitle {...listeners} ref={setActivatorNodeRef}>
                        {groupName}
                    </CardTitle>
                    <Checkbox
                        disabled={references.length <= 0}
                        onCheckedChange={() => {
                            toggleGroup(groupName);
                        }}
                    />
                    <Dialog>
                        <DropdownMenu>
                            <DropdownMenuTrigger>
                                <EllipsisVertical size={14} />
                            </DropdownMenuTrigger>
                            <DropdownMenuContent>
                                <DropdownMenuSub>
                                    <DropdownMenuSubTrigger>
                                        Edit Details
                                    </DropdownMenuSubTrigger>
                                    <DropdownMenuSubContent>
                                        <EditGroupForm groupName={groupName} />
                                    </DropdownMenuSubContent>
                                </DropdownMenuSub>
                                <DropdownMenuSub>
                                    <AlertDialog>
                                        <AlertDialogTrigger>
                                            Delete
                                        </AlertDialogTrigger>
                                        <AlertDialogContent>
                                            <AlertDialogHeader>
                                                <AlertDialogTitle>
                                                    Are you absolutely sure?
                                                </AlertDialogTitle>
                                                <AlertDialogDescription>
                                                    This action cannot be
                                                    undone. This will
                                                    permanently delete the
                                                    group: {groupName}
                                                </AlertDialogDescription>
                                            </AlertDialogHeader>
                                            <AlertDialogFooter>
                                                <AlertDialogCancel>
                                                    Cancel
                                                </AlertDialogCancel>
                                                <AlertDialogAction
                                                    onClick={handleDeleteGroup}
                                                >
                                                    Continue
                                                </AlertDialogAction>
                                            </AlertDialogFooter>
                                        </AlertDialogContent>
                                    </AlertDialog>
                                </DropdownMenuSub>

                                <DropdownMenuSeparator />
                                <DialogTrigger>
                                    <DropdownMenuItem>
                                        Add Selected References
                                    </DropdownMenuItem>
                                </DialogTrigger>
                            </DropdownMenuContent>
                        </DropdownMenu>
                        <DialogContent>
                            <DialogHeader>
                                <DialogTitle>
                                    Adding References into {groupName}
                                </DialogTitle>
                                <DialogDescription>
                                    Add references and/or groups into{" "}
                                    {groupName}. Current limit is 1,000 Nodes
                                    Total
                                </DialogDescription>
                            </DialogHeader>
                            <DialogFooter>
                                <Button
                                    type="submit"
                                    onClick={handleAddReferences}
                                >
                                    Confirm
                                </Button>
                            </DialogFooter>
                        </DialogContent>
                    </Dialog>
                </CardHeader>
                <CardContent
                    ref={setDropNodeRef}
                    style={dropStyle}
                    className={"p-2"}
                >
                    <Collapsible className="group/collapsible [&[data-state=open]>button>svg:first-child]:rotate-90">
                        <SidebarMenuButton
                            disabled={
                                references.length <= 0 && groups.length <= 0
                            }
                        >
                            <CollapsibleTrigger asChild>
                                <ChevronRight className="transition-transform" />
                            </CollapsibleTrigger>
                        </SidebarMenuButton>

                        {references.map((gn: GroupReferenceSchema) => (
                            <CollapsibleContent>
                                <div key={gn.id}>
                                    {" "}
                                    <div>{gn.title}</div>
                                </div>
                            </CollapsibleContent>
                        ))}
                        {groups
                            .filter((g) => g)
                            .map((g: GroupType) => (
                                <CollapsibleContent>
                                    <DraggableGroup
                                        groupName={g.name}
                                        groups={g.groups}
                                        references={g.references}
                                    />
                                </CollapsibleContent>
                            ))}
                    </Collapsible>
                </CardContent>
            </Card>
        </>
    );
}
