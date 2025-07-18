import type { Metadata } from "next";
import Link from "next/link";
import Typography from '@mui/material/Typography';

export const metadata: Metadata = {
  title: "Tuteee",
  description: "Manage lessons, homework and auditing"
};

export default function RootLayout({
  children,
}: Readonly<{
  children: React.ReactNode;
}>) {
  return (
    <html lang="en">
      <body>
        <Link href="/"><Typography variant="h4">🏠</Typography></Link>
        {children}
      </body>
    </html>
  );
}
