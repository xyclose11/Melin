import {
    Card,
    CardContent,
    CardHeader,
    CardTitle,
} from "@/components/ui/card.tsx";

export function DraggableTag({ tagName }: { tagName: string }) {
    return (
        <>
            <Card>
                <CardHeader>
                    <CardTitle>{tagName}</CardTitle>
                </CardHeader>
                <CardContent></CardContent>
            </Card>
        </>
    );
}
