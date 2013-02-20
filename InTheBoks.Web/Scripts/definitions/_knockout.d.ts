interface Knockout
{
	applyBindings(model: any);

	// observables
	observable(): Observable;
	
	// computed observables
	computed(computeFunction: () => any): ComputedObservable;
	computed(computeFunctions: {
		read: () => any;
		write?: (any) => void;
		owner?: any;
		deferEvaluation?: bool;
		disposeWhen?: () => bool;
		disposeWhenNodeIsRemoved?: bool;
	});

	// observable array
	observableArray(): ObservableArray;
	observableArray(intialArray: Array) : ObservableArray;

	// test functions
	isObservable(test: Observable): bool;
	isWritableObservable(test: Observable): bool;
}

interface Observable
{
	(any) : void;
	() : any;
	subscribe(eventHandler: (e: any) => void) : Subscription;
}

interface ComputedObservable extends Observable {
}

interface ObservableArray extends Observable, Array {
	(): Array;

	// additional functions
	remove(item: any): Array;
	remove(itemTest: (item: any) => bool): Array;
	removeAll(): Array;
	removeAll(items: Array): Array;
}

interface Subscription{
	dispose();
}

declare var ko : Knockout;