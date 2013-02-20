/// <reference path="_references.ts" />

module InTheBoks
{
    export interface IEvent {
        on(handler: (data?) => void );
        off(handler: (data?) => void );
    }

    export class Event {
        handlers: { (data?: any): void; }[] = [];

        public on(handler: (data?: any) => void ) {
            this.handlers.push(handler);
        }

        public off(handler: (data?: any) => void ) {
            this.handlers = this.handlers.filter(h => h !== handler);
        }

        public trigger(data?: any) {
            if (this.handlers) {
                this.handlers.forEach(h => h(data));
            }
        }
    }
}