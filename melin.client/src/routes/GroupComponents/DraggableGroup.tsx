import {
    Card,
    CardContent,
    CardHeader,
    CardTitle,
} from "@/components/ui/card.tsx";
import { any, z } from "zod";
import { EllipsisVertical } from "lucide-react";
import { EditGroupForm } from "@/routes/GroupComponents/EditGroupForm.tsx";
import { Button } from "@/components/ui/button.tsx";
import {
    ContextMenu,
    ContextMenuContent,
    ContextMenuItem,
    ContextMenuSeparator,
    ContextMenuSub,
    ContextMenuSubContent,
    ContextMenuSubTrigger,
    ContextMenuTrigger,
} from "@/components/ui/context-menu.tsx";
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

const GroupNodeSchema = z.object({
    id: z.number(),
    title: z.string(),
});

type GroupNodeType = z.infer<typeof GroupNodeSchema>;

export function DraggableGroup({
    groupName,
    groupNodes,
}: {
    groupName: string;
    groupNodes: [];
}) {
    const { selectedReferences } = useReferenceSelection();

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
                // TODO display sonner
                console.log("SUCCESS");
            } else {
                console.error("UNABLE TO ADD REFERENCES TO GROUP");
            }
        } catch (e) {
            console.error(e);
        }
    };
    return (
        <>
            <Card>
                <CardHeader className={"flex"}>
                    <CardTitle>{groupName}</CardTitle>
                    <Dialog>
                        <ContextMenu>
                            <ContextMenuTrigger>
                                <Button
                                    size={"icon"}
                                    className={" p-1 h-fit w-fit"}
                                >
                                    <EllipsisVertical size={12} />
                                </Button>
                            </ContextMenuTrigger>
                            <ContextMenuContent>
                                <ContextMenuSub>
                                    <ContextMenuSubTrigger>
                                        Edit Details
                                    </ContextMenuSubTrigger>
                                    <ContextMenuSubContent>
                                        <EditGroupForm groupName={groupName} />a
                                    </ContextMenuSubContent>
                                </ContextMenuSub>
                                <ContextMenuSeparator />
                                <DialogTrigger>
                                    <ContextMenuItem>
                                        Add References
                                    </ContextMenuItem>
                                </DialogTrigger>
                            </ContextMenuContent>
                        </ContextMenu>
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
                <CardContent>
                    {groupNodes.map((gn: GroupNodeType) => (
                        <div key={gn.id}>
                            <div>{gn.title}</div>
                        </div>
                    ))}
                </CardContent>
            </Card>
        </>
    );
}
