import {
    Card,
    CardContent,
    CardHeader,
    CardTitle,
} from "@/components/ui/card.tsx";
import { useDraggable } from "@dnd-kit/core";

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
                <CardHeader>
                    <CardTitle>Groups</CardTitle>
                </CardHeader>
                <CardContent>{props.children}</CardContent>
            </Card>
        </>
    );
}
