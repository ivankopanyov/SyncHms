import { Parameters } from "../ParameterList/data";

export interface ServiceState {
    isActive: boolean;
    error?: string;
    stackTrace?: string;
}

export interface SimpleServiceInfo {
    name: string;
    parameters: Parameters;
}

export interface ServiceInfo extends SimpleServiceInfo {
    state?: ServiceState;
    timeoutId?: number;
    updateError?: string;
    loading: boolean;
}

export interface ServiceList {
    services: ServiceInfo[];
    serviceCount: number;
    activeServiceCount: number;
    loading: boolean;
    error?: string;
    auth: boolean;
}