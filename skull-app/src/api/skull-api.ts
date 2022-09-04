//----------------------
// <auto-generated>
//     Generated using the NSwag toolchain v13.16.1.0 (NJsonSchema v10.7.2.0 (Newtonsoft.Json v13.0.0.0)) (http://NSwag.org)
// </auto-generated>
//----------------------

/* tslint:disable */
/* eslint-disable */
// ReSharper disable InconsistentNaming

export class Client {
    private http: { fetch(url: RequestInfo, init?: RequestInit): Promise<Response> };
    private baseUrl: string;
    protected jsonParseReviver: ((key: string, value: any) => any) | undefined = undefined;

    constructor(baseUrl?: string, http?: { fetch(url: RequestInfo, init?: RequestInit): Promise<Response> }) {
        this.http = http ? http : window as any;
        this.baseUrl = baseUrl !== undefined && baseUrl !== null ? baseUrl : "";
    }

    /**
     * @return Success
     */
    game(): Promise<IGameState> {
        let url_ = this.baseUrl + "/api/game";
        url_ = url_.replace(/[?&]$/, "");

        let options_: RequestInit = {
            method: "POST",
            headers: {
                "Accept": "text/plain"
            }
        };

        return this.http.fetch(url_, options_).then((_response: Response) => {
            return this.processGame(_response);
        });
    }

    protected processGame(response: Response): Promise<IGameState> {
        const status = response.status;
        let _headers: any = {}; if (response.headers && response.headers.forEach) { response.headers.forEach((v: any, k: any) => _headers[k] = v); };
        if (status === 200) {
            return response.text().then((_responseText) => {
            let result200: any = null;
            let resultData200 = _responseText === "" ? null : JSON.parse(_responseText, this.jsonParseReviver);
            result200 = IGameState.fromJS(resultData200);
            return result200;
            });
        } else if (status !== 200 && status !== 204) {
            return response.text().then((_responseText) => {
            return throwException("An unexpected server error occurred.", status, _responseText, _headers);
            });
        }
        return Promise.resolve<IGameState>(null as any);
    }

    /**
     * @param body (optional) 
     * @return Success
     */
    player(game: string, body: IJoinGameModel | undefined): Promise<IGamePlayerView> {
        let url_ = this.baseUrl + "/api/game/{game}/player";
        if (game === undefined || game === null)
            throw new Error("The parameter 'game' must be defined.");
        url_ = url_.replace("{game}", encodeURIComponent("" + game));
        url_ = url_.replace(/[?&]$/, "");

        const content_ = JSON.stringify(body);

        let options_: RequestInit = {
            body: content_,
            method: "POST",
            headers: {
                "Content-Type": "application/json",
                "Accept": "text/plain"
            }
        };

        return this.http.fetch(url_, options_).then((_response: Response) => {
            return this.processPlayer(_response);
        });
    }

    protected processPlayer(response: Response): Promise<IGamePlayerView> {
        const status = response.status;
        let _headers: any = {}; if (response.headers && response.headers.forEach) { response.headers.forEach((v: any, k: any) => _headers[k] = v); };
        if (status === 200) {
            return response.text().then((_responseText) => {
            let result200: any = null;
            let resultData200 = _responseText === "" ? null : JSON.parse(_responseText, this.jsonParseReviver);
            result200 = IGamePlayerView.fromJS(resultData200);
            return result200;
            });
        } else if (status !== 200 && status !== 204) {
            return response.text().then((_responseText) => {
            return throwException("An unexpected server error occurred.", status, _responseText, _headers);
            });
        }
        return Promise.resolve<IGamePlayerView>(null as any);
    }

    /**
     * @return Success
     */
    view(game: string, player: number): Promise<IGamePlayerView> {
        let url_ = this.baseUrl + "/api/game/{game}/player/{player}/view";
        if (game === undefined || game === null)
            throw new Error("The parameter 'game' must be defined.");
        url_ = url_.replace("{game}", encodeURIComponent("" + game));
        if (player === undefined || player === null)
            throw new Error("The parameter 'player' must be defined.");
        url_ = url_.replace("{player}", encodeURIComponent("" + player));
        url_ = url_.replace(/[?&]$/, "");

        let options_: RequestInit = {
            method: "GET",
            headers: {
                "Accept": "text/plain"
            }
        };

        return this.http.fetch(url_, options_).then((_response: Response) => {
            return this.processView(_response);
        });
    }

    protected processView(response: Response): Promise<IGamePlayerView> {
        const status = response.status;
        let _headers: any = {}; if (response.headers && response.headers.forEach) { response.headers.forEach((v: any, k: any) => _headers[k] = v); };
        if (status === 200) {
            return response.text().then((_responseText) => {
            let result200: any = null;
            let resultData200 = _responseText === "" ? null : JSON.parse(_responseText, this.jsonParseReviver);
            result200 = IGamePlayerView.fromJS(resultData200);
            return result200;
            });
        } else if (status !== 200 && status !== 204) {
            return response.text().then((_responseText) => {
            return throwException("An unexpected server error occurred.", status, _responseText, _headers);
            });
        }
        return Promise.resolve<IGamePlayerView>(null as any);
    }

    /**
     * @param body (optional) 
     * @return Success
     */
    stack(game: string, player: number, body: boolean | undefined): Promise<IGamePlayerView> {
        let url_ = this.baseUrl + "/api/game/{game}/player/{player}/stack";
        if (game === undefined || game === null)
            throw new Error("The parameter 'game' must be defined.");
        url_ = url_.replace("{game}", encodeURIComponent("" + game));
        if (player === undefined || player === null)
            throw new Error("The parameter 'player' must be defined.");
        url_ = url_.replace("{player}", encodeURIComponent("" + player));
        url_ = url_.replace(/[?&]$/, "");

        const content_ = JSON.stringify(body);

        let options_: RequestInit = {
            body: content_,
            method: "POST",
            headers: {
                "Content-Type": "application/json",
                "Accept": "text/plain"
            }
        };

        return this.http.fetch(url_, options_).then((_response: Response) => {
            return this.processStack(_response);
        });
    }

    protected processStack(response: Response): Promise<IGamePlayerView> {
        const status = response.status;
        let _headers: any = {}; if (response.headers && response.headers.forEach) { response.headers.forEach((v: any, k: any) => _headers[k] = v); };
        if (status === 200) {
            return response.text().then((_responseText) => {
            let result200: any = null;
            let resultData200 = _responseText === "" ? null : JSON.parse(_responseText, this.jsonParseReviver);
            result200 = IGamePlayerView.fromJS(resultData200);
            return result200;
            });
        } else if (status !== 200 && status !== 204) {
            return response.text().then((_responseText) => {
            return throwException("An unexpected server error occurred.", status, _responseText, _headers);
            });
        }
        return Promise.resolve<IGamePlayerView>(null as any);
    }

    /**
     * @param body (optional) 
     * @return Success
     */
    challenge(game: string, player: number, body: number | undefined): Promise<IGamePlayerView> {
        let url_ = this.baseUrl + "/api/game/{game}/player/{player}/challenge";
        if (game === undefined || game === null)
            throw new Error("The parameter 'game' must be defined.");
        url_ = url_.replace("{game}", encodeURIComponent("" + game));
        if (player === undefined || player === null)
            throw new Error("The parameter 'player' must be defined.");
        url_ = url_.replace("{player}", encodeURIComponent("" + player));
        url_ = url_.replace(/[?&]$/, "");

        const content_ = JSON.stringify(body);

        let options_: RequestInit = {
            body: content_,
            method: "POST",
            headers: {
                "Content-Type": "application/json",
                "Accept": "text/plain"
            }
        };

        return this.http.fetch(url_, options_).then((_response: Response) => {
            return this.processChallenge(_response);
        });
    }

    protected processChallenge(response: Response): Promise<IGamePlayerView> {
        const status = response.status;
        let _headers: any = {}; if (response.headers && response.headers.forEach) { response.headers.forEach((v: any, k: any) => _headers[k] = v); };
        if (status === 200) {
            return response.text().then((_responseText) => {
            let result200: any = null;
            let resultData200 = _responseText === "" ? null : JSON.parse(_responseText, this.jsonParseReviver);
            result200 = IGamePlayerView.fromJS(resultData200);
            return result200;
            });
        } else if (status !== 200 && status !== 204) {
            return response.text().then((_responseText) => {
            return throwException("An unexpected server error occurred.", status, _responseText, _headers);
            });
        }
        return Promise.resolve<IGamePlayerView>(null as any);
    }
}

export enum Coaster {
    _0 = 0,
    _1 = 1,
}

export class IBid implements IIBid {
    readonly playerId?: number;
    readonly cardsToReveal?: number | undefined;

    constructor(data?: IIBid) {
        if (data) {
            for (var property in data) {
                if (data.hasOwnProperty(property))
                    (<any>this)[property] = (<any>data)[property];
            }
        }
    }

    init(_data?: any) {
        if (_data) {
            (<any>this).playerId = _data["playerId"];
            (<any>this).cardsToReveal = _data["cardsToReveal"];
        }
    }

    static fromJS(data: any): IBid {
        data = typeof data === 'object' ? data : {};
        let result = new IBid();
        result.init(data);
        return result;
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data["playerId"] = this.playerId;
        data["cardsToReveal"] = this.cardsToReveal;
        return data;
    }
}

export interface IIBid {
    playerId?: number;
    cardsToReveal?: number | undefined;
}

export class IGamePlayerView implements IIGamePlayerView {
    readonly playerId?: number;
    readonly nextPlayer?: number;
    readonly opponentStates?: IOpponentState[] | undefined;
    hand?: IReadOnlyHand;
    readonly playedCoasters?: boolean[] | undefined;

    constructor(data?: IIGamePlayerView) {
        if (data) {
            for (var property in data) {
                if (data.hasOwnProperty(property))
                    (<any>this)[property] = (<any>data)[property];
            }
        }
    }

    init(_data?: any) {
        if (_data) {
            (<any>this).playerId = _data["playerId"];
            (<any>this).nextPlayer = _data["nextPlayer"];
            if (Array.isArray(_data["opponentStates"])) {
                (<any>this).opponentStates = [] as any;
                for (let item of _data["opponentStates"])
                    (<any>this).opponentStates!.push(IOpponentState.fromJS(item));
            }
            this.hand = _data["hand"] ? IReadOnlyHand.fromJS(_data["hand"]) : <any>undefined;
            if (Array.isArray(_data["playedCoasters"])) {
                (<any>this).playedCoasters = [] as any;
                for (let item of _data["playedCoasters"])
                    (<any>this).playedCoasters!.push(item);
            }
        }
    }

    static fromJS(data: any): IGamePlayerView {
        data = typeof data === 'object' ? data : {};
        let result = new IGamePlayerView();
        result.init(data);
        return result;
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data["playerId"] = this.playerId;
        data["nextPlayer"] = this.nextPlayer;
        if (Array.isArray(this.opponentStates)) {
            data["opponentStates"] = [];
            for (let item of this.opponentStates)
                data["opponentStates"].push(item.toJSON());
        }
        data["hand"] = this.hand ? this.hand.toJSON() : <any>undefined;
        if (Array.isArray(this.playedCoasters)) {
            data["playedCoasters"] = [];
            for (let item of this.playedCoasters)
                data["playedCoasters"].push(item);
        }
        return data;
    }
}

export interface IIGamePlayerView {
    playerId?: number;
    nextPlayer?: number;
    opponentStates?: IOpponentState[] | undefined;
    hand?: IReadOnlyHand;
    playedCoasters?: boolean[] | undefined;
}

export class IGameState implements IIGameState {
    readonly players?: IPlayer[] | undefined;
    readonly nextPlayer?: number;
    readonly name?: string | undefined;
    phase?: Phase;
    readonly bids?: IBid[] | undefined;

    constructor(data?: IIGameState) {
        if (data) {
            for (var property in data) {
                if (data.hasOwnProperty(property))
                    (<any>this)[property] = (<any>data)[property];
            }
        }
    }

    init(_data?: any) {
        if (_data) {
            if (Array.isArray(_data["players"])) {
                (<any>this).players = [] as any;
                for (let item of _data["players"])
                    (<any>this).players!.push(IPlayer.fromJS(item));
            }
            (<any>this).nextPlayer = _data["nextPlayer"];
            (<any>this).name = _data["name"];
            this.phase = _data["phase"];
            if (Array.isArray(_data["bids"])) {
                (<any>this).bids = [] as any;
                for (let item of _data["bids"])
                    (<any>this).bids!.push(IBid.fromJS(item));
            }
        }
    }

    static fromJS(data: any): IGameState {
        data = typeof data === 'object' ? data : {};
        let result = new IGameState();
        result.init(data);
        return result;
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        if (Array.isArray(this.players)) {
            data["players"] = [];
            for (let item of this.players)
                data["players"].push(item.toJSON());
        }
        data["nextPlayer"] = this.nextPlayer;
        data["name"] = this.name;
        data["phase"] = this.phase;
        if (Array.isArray(this.bids)) {
            data["bids"] = [];
            for (let item of this.bids)
                data["bids"].push(item.toJSON());
        }
        return data;
    }
}

export interface IIGameState {
    players?: IPlayer[] | undefined;
    nextPlayer?: number;
    name?: string | undefined;
    phase?: Phase;
    bids?: IBid[] | undefined;
}

export class IHand implements IIHand {
    playerId?: number;
    cardCount?: number;
    hasSkull?: boolean;

    constructor(data?: IIHand) {
        if (data) {
            for (var property in data) {
                if (data.hasOwnProperty(property))
                    (<any>this)[property] = (<any>data)[property];
            }
        }
    }

    init(_data?: any) {
        if (_data) {
            this.playerId = _data["playerId"];
            this.cardCount = _data["cardCount"];
            this.hasSkull = _data["hasSkull"];
        }
    }

    static fromJS(data: any): IHand {
        data = typeof data === 'object' ? data : {};
        let result = new IHand();
        result.init(data);
        return result;
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data["playerId"] = this.playerId;
        data["cardCount"] = this.cardCount;
        data["hasSkull"] = this.hasSkull;
        return data;
    }
}

export interface IIHand {
    playerId?: number;
    cardCount?: number;
    hasSkull?: boolean;
}

export class IJoinGameModel implements IIJoinGameModel {
    name?: string | undefined;
    firstPlacement?: string | undefined;

    constructor(data?: IIJoinGameModel) {
        if (data) {
            for (var property in data) {
                if (data.hasOwnProperty(property))
                    (<any>this)[property] = (<any>data)[property];
            }
        }
    }

    init(_data?: any) {
        if (_data) {
            this.name = _data["name"];
            this.firstPlacement = _data["firstPlacement"];
        }
    }

    static fromJS(data: any): IJoinGameModel {
        data = typeof data === 'object' ? data : {};
        let result = new IJoinGameModel();
        result.init(data);
        return result;
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data["name"] = this.name;
        data["firstPlacement"] = this.firstPlacement;
        return data;
    }
}

export interface IIJoinGameModel {
    name?: string | undefined;
    firstPlacement?: string | undefined;
}

export class IOpponentHand implements IIOpponentHand {
    readonly cardCount?: number;
    readonly playerId?: number;

    constructor(data?: IIOpponentHand) {
        if (data) {
            for (var property in data) {
                if (data.hasOwnProperty(property))
                    (<any>this)[property] = (<any>data)[property];
            }
        }
    }

    init(_data?: any) {
        if (_data) {
            (<any>this).cardCount = _data["cardCount"];
            (<any>this).playerId = _data["playerId"];
        }
    }

    static fromJS(data: any): IOpponentHand {
        data = typeof data === 'object' ? data : {};
        let result = new IOpponentHand();
        result.init(data);
        return result;
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data["cardCount"] = this.cardCount;
        data["playerId"] = this.playerId;
        return data;
    }
}

export interface IIOpponentHand {
    cardCount?: number;
    playerId?: number;
}

export class IOpponentState implements IIOpponentState {
    readonly playerId?: number;
    readonly name?: string | undefined;
    hand?: IOpponentHand;
    readonly stackCount?: number;

    constructor(data?: IIOpponentState) {
        if (data) {
            for (var property in data) {
                if (data.hasOwnProperty(property))
                    (<any>this)[property] = (<any>data)[property];
            }
        }
    }

    init(_data?: any) {
        if (_data) {
            (<any>this).playerId = _data["playerId"];
            (<any>this).name = _data["name"];
            this.hand = _data["hand"] ? IOpponentHand.fromJS(_data["hand"]) : <any>undefined;
            (<any>this).stackCount = _data["stackCount"];
        }
    }

    static fromJS(data: any): IOpponentState {
        data = typeof data === 'object' ? data : {};
        let result = new IOpponentState();
        result.init(data);
        return result;
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data["playerId"] = this.playerId;
        data["name"] = this.name;
        data["hand"] = this.hand ? this.hand.toJSON() : <any>undefined;
        data["stackCount"] = this.stackCount;
        return data;
    }
}

export interface IIOpponentState {
    playerId?: number;
    name?: string | undefined;
    hand?: IOpponentHand;
    stackCount?: number;
}

export class IPlayer implements IIPlayer {
    readonly playerId?: number;
    playerState?: IPlayerState;
    playerIdentity?: IPlayerIdentity;

    constructor(data?: IIPlayer) {
        if (data) {
            for (var property in data) {
                if (data.hasOwnProperty(property))
                    (<any>this)[property] = (<any>data)[property];
            }
        }
    }

    init(_data?: any) {
        if (_data) {
            (<any>this).playerId = _data["playerId"];
            this.playerState = _data["playerState"] ? IPlayerState.fromJS(_data["playerState"]) : <any>undefined;
            this.playerIdentity = _data["playerIdentity"] ? IPlayerIdentity.fromJS(_data["playerIdentity"]) : <any>undefined;
        }
    }

    static fromJS(data: any): IPlayer {
        data = typeof data === 'object' ? data : {};
        let result = new IPlayer();
        result.init(data);
        return result;
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data["playerId"] = this.playerId;
        data["playerState"] = this.playerState ? this.playerState.toJSON() : <any>undefined;
        data["playerIdentity"] = this.playerIdentity ? this.playerIdentity.toJSON() : <any>undefined;
        return data;
    }
}

export interface IIPlayer {
    playerId?: number;
    playerState?: IPlayerState;
    playerIdentity?: IPlayerIdentity;
}

export class IPlayerIdentity implements IIPlayerIdentity {
    readonly name?: string | undefined;

    constructor(data?: IIPlayerIdentity) {
        if (data) {
            for (var property in data) {
                if (data.hasOwnProperty(property))
                    (<any>this)[property] = (<any>data)[property];
            }
        }
    }

    init(_data?: any) {
        if (_data) {
            (<any>this).name = _data["name"];
        }
    }

    static fromJS(data: any): IPlayerIdentity {
        data = typeof data === 'object' ? data : {};
        let result = new IPlayerIdentity();
        result.init(data);
        return result;
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data["name"] = this.name;
        return data;
    }
}

export interface IIPlayerIdentity {
    name?: string | undefined;
}

export class IPlayerState implements IIPlayerState {
    hand?: IHand;
    readonly playedCoasters?: Coaster[] | undefined;

    constructor(data?: IIPlayerState) {
        if (data) {
            for (var property in data) {
                if (data.hasOwnProperty(property))
                    (<any>this)[property] = (<any>data)[property];
            }
        }
    }

    init(_data?: any) {
        if (_data) {
            this.hand = _data["hand"] ? IHand.fromJS(_data["hand"]) : <any>undefined;
            if (Array.isArray(_data["playedCoasters"])) {
                (<any>this).playedCoasters = [] as any;
                for (let item of _data["playedCoasters"])
                    (<any>this).playedCoasters!.push(item);
            }
        }
    }

    static fromJS(data: any): IPlayerState {
        data = typeof data === 'object' ? data : {};
        let result = new IPlayerState();
        result.init(data);
        return result;
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data["hand"] = this.hand ? this.hand.toJSON() : <any>undefined;
        if (Array.isArray(this.playedCoasters)) {
            data["playedCoasters"] = [];
            for (let item of this.playedCoasters)
                data["playedCoasters"].push(item);
        }
        return data;
    }
}

export interface IIPlayerState {
    hand?: IHand;
    playedCoasters?: Coaster[] | undefined;
}

export class IReadOnlyHand implements IIReadOnlyHand {
    readonly cardCount?: number;
    readonly playerId?: number;
    readonly hasSkull?: boolean;

    constructor(data?: IIReadOnlyHand) {
        if (data) {
            for (var property in data) {
                if (data.hasOwnProperty(property))
                    (<any>this)[property] = (<any>data)[property];
            }
        }
    }

    init(_data?: any) {
        if (_data) {
            (<any>this).cardCount = _data["cardCount"];
            (<any>this).playerId = _data["playerId"];
            (<any>this).hasSkull = _data["hasSkull"];
        }
    }

    static fromJS(data: any): IReadOnlyHand {
        data = typeof data === 'object' ? data : {};
        let result = new IReadOnlyHand();
        result.init(data);
        return result;
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data["cardCount"] = this.cardCount;
        data["playerId"] = this.playerId;
        data["hasSkull"] = this.hasSkull;
        return data;
    }
}

export interface IIReadOnlyHand {
    cardCount?: number;
    playerId?: number;
    hasSkull?: boolean;
}

export enum Phase {
    _0 = 0,
    _1 = 1,
    _2 = 2,
    _3 = 3,
    _4 = 4,
}

export class ApiException extends Error {
    message: string;
    status: number;
    response: string;
    headers: { [key: string]: any; };
    result: any;

    constructor(message: string, status: number, response: string, headers: { [key: string]: any; }, result: any) {
        super();

        this.message = message;
        this.status = status;
        this.response = response;
        this.headers = headers;
        this.result = result;
    }

    protected isApiException = true;

    static isApiException(obj: any): obj is ApiException {
        return obj.isApiException === true;
    }
}

function throwException(message: string, status: number, response: string, headers: { [key: string]: any; }, result?: any): any {
    if (result !== null && result !== undefined)
        throw result;
    else
        throw new ApiException(message, status, response, headers, null);
}