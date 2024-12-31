import {
    Card,
    CardContent,
    CardFooter,
    CardHeader,
    CardTitle,
    CardDescription,
} from "@/components/ui/card.tsx";

export function FormattedView() {
    return (
        <div>
            <Card>
                <CardHeader>
                    <CardTitle>Formatted View</CardTitle>
                    <CardDescription>
                        Here is the formatted view!
                    </CardDescription>
                </CardHeader>
                <CardContent>CONTENT</CardContent>
                <CardFooter>FOOTER</CardFooter>
            </Card>
        </div>
    );
}
