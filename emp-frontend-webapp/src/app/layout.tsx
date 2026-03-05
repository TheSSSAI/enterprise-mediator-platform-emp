import type { Metadata } from 'next';
import { Inter } from 'next/font/google';
import { cn } from '@/lib/utils';
import '@/app/globals.css'; // Assuming global CSS exists as standard Next.js setup

const inter = Inter({ subsets: ['latin'], variable: '--font-inter' });

export const metadata: Metadata = {
  title: {
    template: '%s | Enterprise Mediator',
    default: 'Enterprise Mediator Platform',
  },
  description: 'Enterprise-grade project mediation and vendor management platform.',
  icons: {
    icon: '/favicon.ico',
  },
};

interface RootLayoutProps {
  children: React.ReactNode;
}

export default function RootLayout({ children }: RootLayoutProps) {
  return (
    <html lang="en" className="h-full">
      <body
        className={cn(
          'h-full bg-background font-sans antialiased text-foreground',
          inter.variable
        )}
      >
        <main className="h-full w-full">
          {children}
        </main>
      </body>
    </html>
  );
}