import {
    Card,
    CardContent,
    CardFooter,
    CardHeader,
    CardTitle,
    CardDescription,
} from "@/components/ui/card.tsx";

export function RawView() {
    return (
        <div>
            <Card>
                <CardHeader>
                    <CardTitle>Raw View</CardTitle>
                    <CardDescription>Here is the Raw view!</CardDescription>
                </CardHeader>
                <CardContent>CONTENT</CardContent>
                <CardFooter>FOOTER</CardFooter>
            </Card>
        </div>
    );
}
