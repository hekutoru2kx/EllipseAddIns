
package com.mincom.enterpriseservice.screen;

import javax.xml.ws.WebFault;


/**
 * This class was generated by the JAX-WS RI.
 * JAX-WS RI 2.2.6-1b01 
 * Generated source version: 2.2
 * 
 */
@WebFault(name = "EnterpriseServiceException", targetNamespace = "http://screen.enterpriseservice.mincom.com")
public class EnterpriseServiceException
    extends Exception
{

    /**
     * Java type that goes as soapenv:Fault detail element.
     * 
     */
    private com.mincom.enterpriseservice.exception.EnterpriseServiceException faultInfo;

    /**
     * 
     * @param message
     * @param faultInfo
     */
    public EnterpriseServiceException(String message, com.mincom.enterpriseservice.exception.EnterpriseServiceException faultInfo) {
        super(message);
        this.faultInfo = faultInfo;
    }

    /**
     * 
     * @param message
     * @param faultInfo
     * @param cause
     */
    public EnterpriseServiceException(String message, com.mincom.enterpriseservice.exception.EnterpriseServiceException faultInfo, Throwable cause) {
        super(message, cause);
        this.faultInfo = faultInfo;
    }

    /**
     * 
     * @return
     *     returns fault bean: com.mincom.enterpriseservice.exception.EnterpriseServiceException
     */
    public com.mincom.enterpriseservice.exception.EnterpriseServiceException getFaultInfo() {
        return faultInfo;
    }

}