import { Card } from "@/components/ui/card.tsx";

export function ChoiceCard({ options, linkTo }: any) {
    return (
        <>
            <Card>
                <a
                    href={linkTo}
                    className="flex w-full flex-col items-center rounded-xl border bg-card p-6 text-card-foreground shadow transition-colors hover:bg-muted/50 sm:p-10"
                >
                    {options}
                </a>
            </Card>
        </>
    );
}
